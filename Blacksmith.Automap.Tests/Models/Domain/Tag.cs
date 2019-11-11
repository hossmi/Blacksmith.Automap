using System;
using Blacksmith.Validations.Exceptions;

namespace Blacksmith.Automap.Tests.Models.Domain
{
    public class Tag : AbstractAppDomain, IEquatable<Tag>
    {
        private string name;

        public Tag(string name) : base()
        {
            this.Name = name;
        }

        public string Name
        {
            get => this.name;
            set
            {
                base.stringIsNotEmpty<DomainException>(value);
                this.name = value;
            }
        }

        public bool Equals(Tag other)
        {
            return this.name == other.name;
        }
    }
}