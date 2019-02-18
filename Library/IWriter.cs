using System.IO;

namespace Library
{
    public interface IWriter<in T>
    {
        void Write(T value, Stream stream);
    }
}