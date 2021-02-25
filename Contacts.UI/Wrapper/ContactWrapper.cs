using Contacts.Model;
using System;
using System.Collections.Generic;

namespace Contacts.UI.Wrapper
{
    public class ContactWrapper : ModelWrapper<Contact>
    {
        public ContactWrapper(Contact model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "invalid", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Validation to implement";
                    }
                    break;
            }
        }
    }
}
