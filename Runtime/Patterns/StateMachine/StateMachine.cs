using System;
using System.Collections.Generic;

namespace Bounce.Framework
{
    /// <summary>
    /// A lightweight state machine.
    /// </summary>
    /// <remarks>
    /// 	<para>To use it: </para>
    /// 	<list type="bullet">
    /// 		<item>
    /// 			<description>Define your own label. Enums are probably the best
    /// choice.</description>
    /// 		</item>
    /// 		<item>
    /// 			<description>Construct a new state machine, typically in a
    /// MonoBehaviour's Start method.</description>
    /// 		</item>
    /// 		<item>
    /// 			<description>Add the various states with the appropriate delegates.
    /// </description>
    /// 		</item>
    /// 		<item>
    /// 			<description>Call the state machine's Update method from the
    /// MonoBehaviour's Update method.</description>
    /// 		</item>
    /// 		<item>
    /// 			<description>Set the CurrentState property on the state machine to transition.
    /// (You can either set it爁rom one of the state delegates, or from anywhere else.
    /// </description>
    /// 		</item>
    /// 	</list>
    /// 	<para>When a state is changed, the OnStop on existing state is called, then the
    /// OnStart of the new state, and from there on OnUpdate of the new state each time
    /// the update is called.</para>
    /// </remarks>
    /// <typeparam name="TLabel">The label type of this state machine. Enums are common,
    /// but strings or int are other possibilities.</typeparam>
    public class StateMachine<TLabel>
    {
        #region  Types

        private class State
        {
            #region Public Fields

            public readonly TLabel label;
            public readonly Action onStart;
            public readonly Action onStop;
            public readonly Action onUpdate;

            #endregion

            #region Constructors

            public State(TLabel label, Action onStart, Action onUpdate, Action onStop)
            {
                this.onStart = onStart;
                this.onUpdate = onUpdate;
                this.onStop = onStop;
                this.label = label;
            }

            #endregion
        }

        #endregion

        #region Private Fields

        private readonly Dictionary<TLabel, State> stateDictionary;
        private State currentState;
        private State previousState;

        #endregion

        #region  Properties

        public TLabel PreviousState
        {
            get { return previousState == null ? currentState.label : previousState.label; }
        }

        /// <summary>
        /// Returns the label of the current state.
        /// </summary>
        public TLabel CurrentState
        {
            get { return currentState.label; }

            set { ChangeState(value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new StateMachine.
        /// </summary>
        public StateMachine()
        {
            stateDictionary = new Dictionary<TLabel, State>();
        }

        #endregion

        #region Unity Callbacks

        /// <summary>
        /// This method should be called every frame.
        /// </summary>
        public void Update()
        {
            if (currentState != null && currentState.onUpdate != null)
            {
                currentState.onUpdate();
            }
        }

        #endregion

        #region Public Methods

        public void AddState(TLabel label, IStateMachine stateMachine)
        {
            stateMachine.Init();

            stateDictionary[label] = new State(label, stateMachine.OnStart, stateMachine.OnUpdate, stateMachine.OnStop);
        }

        /// <summary>
        /// Adds a state, and the delegates that should run 
        /// when the state starts, stops, 
        /// and when the state machine is updated.
        /// 
        /// Any delegate can be null, and wont be executed.
        /// </summary>
        /// <param name="label">The name of the state to add.</param>
        /// <param name="onStart">The action performed when the state is entered.</param>
        /// <param name="onUpdate">The action performed when the state machine is updated in the given state.</param>
        /// <param name="onStop">The action performed when the state machine is left.</param>
        public void AddState(TLabel label, Action onStart, Action onUpdate, Action onStop)
        {
            stateDictionary[label] = new State(label, onStart, onUpdate, onStop);
        }

        /// <summary>
        /// Adds a state, and the delegates that should run 
        /// when the state starts, 
        /// and when the state machine is updated.
        /// 
        /// Any delegate can be null, and wont be executed.
        /// </summary>
        /// <param name="label">The name of the state to add.</param>
        /// <param name="onStart">The action performed when the state is entered.</param>
        /// <param name="onUpdate">The action performed when the state machine is updated in the given state.</param>
        public void AddState(TLabel label, Action onStart, Action onUpdate)
        {
            AddState(label, onStart, onUpdate, null);
        }

        /// <summary>
        /// Adds a state, and the delegates that should run 
        /// when the state starts.
        /// 
        /// Any delegate can be null, and wont be executed.
        /// </summary>
        /// <param name="label">The name of the state to add.</param>
        /// <param name="onStart">The action performed when the state is entered.</param>
        public void AddState(TLabel label, Action onStart)
        {
            AddState(label, onStart, null);
        }

        /// <summary>
        /// Adds a state.
        /// </summary>
        /// <param name="label">The name of the state to add.</param>
        public void AddState(TLabel label)
        {
            AddState(label, null, null);
        }

        /// <summary>
        /// Adds a sub state machine for the given state.
        ///
        /// The sub state machine need not be updated, as long as this state machine
        /// is being updated.
        /// </summary>
        /// <typeparam name="TSubStateLabel">The type of the sub-machine.</typeparam>
        /// <param name="label">The name of the state to add.</param>
        /// <param name="subMachine">The sub-machine that will run during the given state.</param>
        /// <param name="subMachineStartState">The starting state of the sub-machine.</param>
        public void AddState<TSubStateLabel>(TLabel label, StateMachine<TSubStateLabel> subMachine,
            TSubStateLabel subMachineStartState)
        {
            AddState(
                label,
                () => subMachine.ChangeState(subMachineStartState),
                subMachine.Update);
        }

        /// <summary>
        /// Returns the current state name
        /// </summary>
        public override string ToString()
        {
            return CurrentState.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Changes the state from the existing one to the state with the given label.
        /// 
        /// It is legal (and useful) to transition to the same state, in which case the 
        /// current state's onStop action is called, the onStart action is called, and the
        /// state keeps on updating as before. The behaviour is exactly the same as switching to
        /// a new state.
        /// </summary>
        private void ChangeState(TLabel newState)
        {
            previousState = currentState;

            if (currentState != null && currentState.onStop != null)
            {
                currentState.onStop();
            }

            currentState = stateDictionary[newState];

            currentState.onStart?.Invoke();

            OnStateChanged?.Invoke();
        }

        #endregion

        #region Callback Events

        public event Action OnStateChanged;

        #endregion
    }
}