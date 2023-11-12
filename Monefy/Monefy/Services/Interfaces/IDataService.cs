using Monefy.Models;

namespace Monefy.Services.Interfaces
{
    interface IDataService
    {
        public void SendData(object data);
        public void SendData(object[] data);
    }
}