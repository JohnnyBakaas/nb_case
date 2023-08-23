import styles from "./AgeInput.module.css";

interface AgeInputProps {
  optional?: boolean;
  val: number;
  setVal: (val: number) => void;
}

const AgeInput = ({ val, setVal, optional }: AgeInputProps) => {
  const displayAge = val !== 0 ? val : "";
  return (
    <div className={styles.wrapper}>
      <label htmlFor="ageInput" className={styles.label}>
        {optional === undefined ? "*" : null} Alder
      </label>
      <input
        className={styles.input}
        type="number"
        min="0"
        value={displayAge}
        onChange={(e) => {
          setVal(Number(e.target.value));
        }}
      />
    </div>
  );
};

export default AgeInput;
