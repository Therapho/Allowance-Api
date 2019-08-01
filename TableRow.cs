using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions
{
    public abstract class TableRow<T> : TableEntity 
        where T : Entity 
    {
        public T Entity
        {
            get
            {
                return MapToEntity();
            }
            set
            {
                MapFromEntity(value);
            }
        }

        protected abstract T MapToEntity();
        protected abstract void MapFromEntity(T entity);
    }
}
