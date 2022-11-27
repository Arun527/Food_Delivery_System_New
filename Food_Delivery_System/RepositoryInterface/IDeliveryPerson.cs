using Food_Delivery.Models;


namespace Food_Delivery.RepositoryInterface
{
    public interface IDeliveryPerson
    {
        public IEnumerable<DeliveryPerson> GetAllDeliveryPersons();
        public DeliveryPerson GetDeliveryPerson(int deliveryPersonId);
        public DeliveryPerson GetDeliveryPersonByNumber(string Number);
        public Messages InsertDeliveryPerson(DeliveryPerson deliveryPerson);
        public Messages UpdateDeliveryPerson(DeliveryPerson deliveryPerson);
        public Messages DeleteDeliveryPerson(int deliveryPersonId);

      //  public LoginDto loginbyid(string contactNumber, string password);

    }
}
