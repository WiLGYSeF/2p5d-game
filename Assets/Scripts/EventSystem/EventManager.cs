using System.Collections.Generic;

namespace EventSystemNS {
    public class EventManager : Utility.Singleton<EventManager> {
        private static Dictionary<
            string,
            HashSet<System.Action<System.EventArgs>>
        > _listeners = new Dictionary<string, HashSet<System.Action<System.EventArgs>>>();

        public override void SingletonInit() {}
        public override void SingletonDestroy() {}

        public static void AddListener(string name, System.Action<System.EventArgs> action) {
            HashSet<System.Action<System.EventArgs>> actions;
            if(!_listeners.TryGetValue(name, out actions)) {
                actions = new HashSet<System.Action<System.EventArgs>>();
                _listeners.Add(name, actions);
            }
            actions.Add(action);
        }

        public static void RemoveListener(string name, System.Action<System.EventArgs> action) {
            HashSet<System.Action<System.EventArgs>> actions;
            if(_listeners.TryGetValue(name, out actions)) {
                actions.Remove(action);
            }
        }

        public static void Dispatch(string name, System.EventArgs args) {
            HashSet<System.Action<System.EventArgs>> actions;
            if (_listeners.TryGetValue(name, out actions)) {
                for (var emtr = actions.GetEnumerator(); emtr.MoveNext(); ) {
                    emtr.Current.Invoke(args);
                }
            }
        }
    }
}
