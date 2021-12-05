namespace Utils.Common {
    public class ReferenceableVariable<T> {
        private T value;
        public T Value { get => value; set => this.value = value; }
        public T StartValue { get; }
        public ReferenceableVariable(T startValue) {
            value = startValue;
            StartValue = startValue;
        }
    }
}
