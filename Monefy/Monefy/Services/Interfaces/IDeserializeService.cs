using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

interface IDeserializeService
{
    public ObservableCollection<T> Deserialize<T>(string path);
}
