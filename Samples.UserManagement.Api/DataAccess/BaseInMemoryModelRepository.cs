using System;
using Samples.UserManagement.Api.Extensions;
using Samples.UserManagement.Api.Models;
using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api.DataAccess
{
    public abstract class BaseInMemoryModelRepository<T> : BaseInMemoryRepository<T, int>, IBaseModelRepository<T>
        where T : BaseModel
    {
        private readonly ISequenceProvider _sequenceProvider;

        protected BaseInMemoryModelRepository(ISequenceProvider sequenceProvider)
        {
            _sequenceProvider = sequenceProvider;
        }

        public override int Add(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Id != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(model));
            }

            model.Id = _sequenceProvider.Next<T>();

            OnAddOrUpdate(model);

            return base.Add(model);
        }

        public override T Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Id == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(model));
            }

            if (!_dbModels.TryGetValue(model.Id, out var existing))
            {
                throw new ApplicationException($"Record does not exist [{typeof(T).Name}].[{model.Id}]");
            }

            model.CreatedOn = existing.CreatedOn;

            OnAddOrUpdate(model);

            return base.Update(model);
        }

        private void OnAddOrUpdate(T model)
        {
            model.ModifiedOn = DateTime.UtcNow;

            if (model.CreatedOn == default)
            {
                model.CreatedOn = model.ModifiedOn;
            }
        }
    }
}
