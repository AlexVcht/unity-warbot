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
        connaissances.Add(new Connaissance(obj));
    }

    public class Connaissance
    {
        private GameObject agent;
        private long insertedAt;

        public Connaissance(GameObject p_agent)
        {
            agent = p_agent;
            insertedAt = Environment.TickCount;
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
