namespace Infrastructure.Domain.Model
{
    public abstract class BaseEntity : IEntity
    {
        public virtual int Id { get; set; }

        //public virtual string DisplayName { get; set; }
        public virtual  string Name { get; set; }

        #region Equality

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var entity = obj as BaseEntity;
            return entity != null && Id.Equals(entity.Id);
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            return !ReferenceEquals(left, null) && left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion Equality
    }
}