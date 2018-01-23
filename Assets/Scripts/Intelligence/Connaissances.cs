using System;
using System.Collections;
using UnityEngine;

public class Connaissances
{
    public ArrayList connaissances;

    public Connaissances()
    {
        connaissances = new ArrayList();
    }

    public bool ContainsCustom(Rigidbody rigidbody)
    {
        foreach(Connaissance c in connaissances)
        {
            if (c.getAgent() == rigidbody)
                return true;
        }

        return false;
    }

    public void RemoveCustom(Rigidbody rigidbody)
    {
        Connaissance cTmp = null;

        foreach (Connaissance c in connaissances)
        {
            if (c.getAgent() == rigidbody)
                cTmp = c;
        }

        if (cTmp != null)
            connaissances.Remove(cTmp);
    }

    public void Reset()
    {
        connaissances.Clear();
    }

    public class Connaissance
    {
        private readonly Rigidbody agent;
        private readonly long insertedAt;

        public Connaissance(Rigidbody p_agent)
        {
            agent = p_agent;
            insertedAt = Environment.TickCount;
        }

        public Rigidbody getAgent()
        {
            return agent;
        }

        public long getInsertedAt()
        {
            return insertedAt;
        }
    }
}
