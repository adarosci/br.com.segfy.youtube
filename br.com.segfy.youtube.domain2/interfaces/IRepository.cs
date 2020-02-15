using System;
using System.Collections.Generic;
using System.Text;

namespace br.com.segfy.youtube.domain.interfaces
{
    public interface IRepository
    {
        void Save<T>(T obj, string collection);
        void Save<T>(IEnumerable<T> obj, string collection);
        IEnumerable<T> Load<T>(string collection);
        IEnumerable<T> Load<T>(Func<T, bool> predicate, string collection);
    }
}
