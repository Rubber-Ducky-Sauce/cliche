using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new file", menuName = "Dialog", order = 1)]
public class Dialog : ScriptableObject
{
    [SerializeField]
    public List<string> actors;
    [Serializable]
    public struct Line
    {
        [SerializeField][Range(0,6)]public int actor;
        public string line;
        

        public Line(int actor, string line)
        {
            this.actor = actor;
            this.line = line;
        }
    }

    [SerializeField]public List<Line> ScriptDialog;

}



