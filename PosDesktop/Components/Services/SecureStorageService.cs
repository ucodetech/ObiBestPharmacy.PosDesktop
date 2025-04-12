using PosDesktop.Components.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosDesktop.Components.Services;

public class SecureStorageService : ISecureStorageService
{
    public void Clear() => SecureStorage.Default.RemoveAll();

  
    public async Task<string> GetAsync(string key) => await SecureStorage.Default.GetAsync(key);


    public void Remove(string key) =>  SecureStorage.Default.Remove(key);

    public Task SetAsync(string key, string value) => SecureStorage.Default.SetAsync(key, value);
}
