using System;
using System.Collections.Generic;

public interface ISaveable
{
    string GetGUID();
    void Save();
    void Load();
}

/*
public class SaveManager
{
    
}


public class foo
{
    private Dictionary<string, string> SaveData;
    
    public string[] getData()
    {
            
    }
    public void load()
    {
        Guid id;
        if (id == Guid.Empty)
        {
            while () 
        }
        
        if (SaveData.ContainsKey(id))
        {
            
        }
    }
}
*/