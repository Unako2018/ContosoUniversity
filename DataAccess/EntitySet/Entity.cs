using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess.EntitySet
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime? UpdatedDate { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() * 907 + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }

        #region SerializationMask

        [NotMapped][JsonIgnore] public bool SerializeId { get; protected set; } = false;
        [NotMapped][JsonIgnore] public bool SerializeCreatedDate { get; protected set; } = false;
        [NotMapped][JsonIgnore] public bool SerializeUpdatedDate { get; protected set; } = false;

        public bool ShouldSerializeId() => SerializeId;
        public bool ShouldSerializeCreatedDate() => SerializeCreatedDate;
        public bool ShouldSerializeUpdatedDate() => SerializeUpdatedDate;

        #endregion

    }

}
