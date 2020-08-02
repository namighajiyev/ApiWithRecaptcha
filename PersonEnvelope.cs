namespace ApiWithRecaptcha
{
    public class PersonEnvelope
    {

        public PersonEnvelope(Person person)
        {
            this.Person = person;
        }

        public Person Person { get; private set; }
    }
}