using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    interface IData
    {
    }

    class ClassForIData : IData
    {
        public IClient cl;
        public ClassForIData(IClient _cl)
        {
            cl = _cl;
        }
    }

    interface ISmth
    {
    }

    class ClassForISmth : ISmth
    {
    }

    interface IService
    {
    }

    class FirstForIService : IService
    {
        public ISmth smth;

        public FirstForIService() { }
        public FirstForIService(ISmth _smth)
        {
            smth = _smth;
        }
    }

    class SecondClForIService : IService
    {
    }

    interface IClient
    {
    }

    class ClassForIClient : IClient
    {
        public IData data;
        public ClassForIClient(IData _data)
        {
            data = _data;
        }
    }

    class SecondClassForIClent : IClient
    {
        public IEnumerable<IService> serv = null;

        public SecondClassForIClent(IEnumerable<IService> _serv)
        {
            serv = _serv;
        }
    }
}
