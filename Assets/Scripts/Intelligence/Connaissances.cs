using System;
using System.Collections;
using UnityEngine;

public class Connaissances
{
    private ArrayList connaissances;

    public Connaissances()
    {
        connaissances = new ArrayList();
    }

    public void add(Rigidbody obj)
    {
        if(obj == null)
        {
            return;
        }
        connaissances.Add(new Connaissance(obj));
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
