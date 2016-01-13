using System;

namespace ManahostManager.Domain.Entity
{
    public interface IEntity
    {
        int? GetFK();

        void SetDateModification(DateTime? d);
    }
}