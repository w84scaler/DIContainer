using System;

namespace DIContainer
{
    public class ImplementationInfo
    {
        public readonly Type implClassType;
        public readonly LifeTime lifeTime;

        public ImplementationInfo(LifeTime lt, Type impl)
        {
            implClassType = impl;
            lifeTime = lt;
        }
    }
}