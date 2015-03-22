namespace Infrastructure.Domain
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Domain.Model;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly List<Operation> operations = new List<Operation>();

        public void RegisterAdd(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!this.operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this.operations.Add(
                    new Operation
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
            if (!this.operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this.operations.Add(
                    new Operation
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
            if (!this.operations.Exists(op => (op.AggregateRoot.Id == aggregateRoot.Id)))
            {
                this.operations.Add(
                    new Operation
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
            //using (var scope = new TransactionScope())
            //{
            foreach (var operation in this.operations)
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
            this.operations.Clear();
            //    scope.Complete();
            //}
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