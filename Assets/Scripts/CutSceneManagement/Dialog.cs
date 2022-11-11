using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new file", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    [SerializeField]
    public List<string> actors;

    [SerializeField]public List<Line> ScriptDialog;

    [Serializable]
    public struct Line
    {
        [SerializeField][Range(0, 10)] public int actor;
        public string line;


        public Line(int actor, string line)
        {
            this.actor = actor;
            this.line = line;
        }
    }

}



