using System.IO;

namespace Library
{
    public interface IReader<out T>
    {
        T Read(Stream stream);
    }
}