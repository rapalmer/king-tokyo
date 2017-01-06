
namespace DataStructures.Observer_Pattern
{
    public abstract class Observer<T> where T : Observable<T>
    {
        internal bool Skip; //Skip this observer during the next update?
        private readonly bool _shouldSkipAfterUpdate;  //Does this observer modify a property? [yes = true | no = false]

        /// <summary>
        /// Updates the property values of its subscriptions whenever the subscription is modified
        /// </summary>
        /// <param name="shouldSkipAfterUpdate">Does this observer modify a property?</param>
        protected Observer(bool shouldSkipAfterUpdate = true)
        {
            _shouldSkipAfterUpdate = shouldSkipAfterUpdate;
        }

        /// <summary>
        /// Update the given subscription
        /// </summary>
        /// <param name="observable">Subscription</param>
        public void Update(T observable)
        {
            if (!Skip)
            {
                Skip = _shouldSkipAfterUpdate;
                UpdateLogic(observable);
            }
            else
            {
                Skip = false;
            }
        }

        /// <summary>
        /// Does the given subscription meet the criteria to update?
        /// </summary>
        /// <param name="observable">Subscription</param>
        /// <returns>Should this observer modifty the subscription</returns>
        public abstract bool UpdateCondition(T observable);

        /// <summary>
        /// How this observer changes the given subscription
        /// </summary>
        /// <param name="observable">Subscription</param>
        protected abstract void UpdateLogic(T observable);
    }
}