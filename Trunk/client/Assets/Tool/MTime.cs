using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTime  {
    private System.DateTime _currentTime = new System.DateTime();
    private int _intervalTime = 0;
    private long _lastCycle = 0;
    public MTime() {
        Now();
    }
    public int IntervalTime {
        get { return _intervalTime; }
        set { _intervalTime = value; }
    }

    public bool Cycle() {
        long now = System.DateTime.Now.Ticks/10000;
        if (now - _lastCycle > _intervalTime) {
            _lastCycle = now;
            return true;
        }
        return false;
    }
    // 毫秒
    public long  Ticks() {
        //return (long)Convert.ToSingle(_currentTime - System.DateTime.Parse("1970-1-1")).TotalMilliseconds;// / 10000;
        return _currentTime.Ticks;
    }
    public void Now() {
        _currentTime = System.DateTime.Now;
    }
    public void Refresh() {
       
    }
    // 毫秒
    public long DifferTicks(MTime mtime) {
        return Ticks() - mtime.Ticks();
    }
    public long Differ(MTime mtime)
    {
        return _currentTime.Ticks - mtime._currentTime.Ticks;
    }



  
}
