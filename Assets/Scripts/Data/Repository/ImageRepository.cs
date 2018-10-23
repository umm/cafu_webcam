using System;
using System.Linq;
using System.Text;
using CAFU.Data.Data.Repository;
using CAFU.WebCam.Domain.Structure.Data;
using CAFU.WebCam.Domain.UseCase;
using UniRx;
using UnityEngine;

namespace CAFU.WebCam.Data.Repository
{
    public class ImageRepository : ObservableRWRepository, IObservableImageRWHandler
    {
        IObservable<StorableTexture> IObservableImageRWHandler.ReadAsObservable(Uri uri)
        {
            return Observable
                .Zip(
                    base.ReadAsObservable(new UriBuilder(uri) {Path = $"{uri.AbsolutePath}.info.bytes"}.Uri),
                    base.ReadAsObservable(new UriBuilder(uri) {Path = $"{uri.AbsolutePath}.data.bytes"}.Uri)
                )
                .Select(
                    xs =>
                    {
                        // 1つ目を Serialize された JSON として Deserialize
                        var entity = JsonUtility.FromJson<StorableTexture>(Encoding.UTF8.GetString(xs[0].ToArray()));
                        // 2つ目を画像データとしてセット
                        entity.Data = xs[1];
                        return entity;
                    }
                );
        }

        public IObservable<Unit> WriteAsObservable(Uri uri, StorableTexture entity)
        {
            return Observable
                .Zip(
                    // SerializeField を JSON に Serialize してから保存
                    WriteAsObservable(new UriBuilder(uri) {Path = $"{uri.AbsolutePath}.info.bytes"}.Uri, Encoding.UTF8.GetBytes(JsonUtility.ToJson(entity))),
                    // 画像データを保存
                    WriteAsObservable(new UriBuilder(uri) {Path = $"{uri.AbsolutePath}.data.bytes"}.Uri, entity.Data)
                )
                .AsUnitObservable();
        }
    }
}