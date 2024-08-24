// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using LiteDB;

namespace DataStandardRepository.Actions.LiteDB;
public static class CollectionRetrieval<T>
{
    public static IEnumerable<T> GetCollection(LiteDatabase db, string collectionName)
    {
        return db.GetCollection<T>(collectionName).FindAll();
    }
}
