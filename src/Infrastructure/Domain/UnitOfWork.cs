using Infrastructure.Domain.Model;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly List<Operation> _operations = new List<Operation>();

        public void RegisterAdd(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!this._operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this._operations.Add(new Operation
                     {
                         Type = Operation.OperationType.Insert,
                         AggregateRoot = aggregateRoot,
                         ProcessDate = DateTime.Now,
                         Repository = repository
                     });
            }
        }

        public void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!this._operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this._operations.Add(new Operation
                    {
                        Type = Operation.OperationType.Update,
                        AggregateRoot = aggregateRoot,
                        ProcessDate = DateTime.Now,
                        Repository = repository
                    });
            }
        }

        public void RegisterRemoved(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!this._operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this._operations.Add(new Operation
                    {
                        Type = Operation.OperationType.Remove,
                        AggregateRoot = aggregateRoot,
                        ProcessDate = DateTime.Now,
                        Repository = repository
                    });
            }
        }

        public virtual void Commit()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var operation in this._operations)
                {
                    switch (operation.Type)
                    {
                        case Operation.OperationType.Insert:
                            operation.Repository.PersistNewItem(operation.AggregateRoot);
                            break;

                        case Operation.OperationType.Remove:
                            operation.Repository.PersistRemoveItem(operation.AggregateRoot);
                            break;

                        case Operation.OperationType.Update:
                            operation.Repository.PersistUpdateItem(operation.AggregateRoot);
                            break;
                    }
                }
                this._operations.Clear();
                scope.Complete();
            }
        }
    }

    internal sealed class Operation
    {
        public enum OperationType
        {
            Insert,
            Update,
            Remove
        }

        public OperationType Type { get; set; }

        public IAggregateRoot AggregateRoot { get; set; }

        public DateTime ProcessDate { get; set; }

        public IUnitOfWorkRepository Repository { get; set; }
    }
}