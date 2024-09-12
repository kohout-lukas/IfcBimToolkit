// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

using LiteDB;

namespace DataStandardRepository.Actions.LiteDB;
public static class CollectionRetrieval<T>
{
    public static IEnumerable<T> GetCollection(LiteDatabase db, string collectionName)
    {
        return db.GetCollection<T>(collectionName).FindAll();
    }
}
