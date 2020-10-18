using System;

namespace VISMA.TestTask.Data
{
    public interface IFakeDataCollection : IDisposable
    {
        void Load();
    }
}