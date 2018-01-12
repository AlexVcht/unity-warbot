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

    public void add(GameObject obj)
    {
        if(obj == null)
        {
            return;
        }
        connaissances.Add(new Connaissance(obj, Environment.TickCount);
    }

    public class Connaissance
    {
        private GameObject agent;
        private long insertedAt;

        public Connaissance(GameObject p_agent, long p_insertedAt)
        {
            agent = p_agent;
            insertedAt = p_insertedAt;
        }

        public GameObject getAgent()
        {
            return agent;
        }

        public long getInsertedAt()
        {
            return insertedAt;
        }
    }
}
