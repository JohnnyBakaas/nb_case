import styles from "./TlfInput.module.css";

interface TlfInputProps {
  optional?: boolean;
  cuntryCode: number;
  tlf: number;
  setCountryCide: (cc: number) => void;
  setTlf: (tlf: number) => void;
}

const TlfInput = ({
  optional,
  cuntryCode,
  tlf,
  setCountryCide,
  setTlf,
}: TlfInputProps) => {
  return (
    <div className={styles.wrapper}>
      <label>{optional ? null : "* "}Telefon nr.</label>
      <div style={{ display: "flex", justifyContent: "space-between" }}>
        <input
          placeholder="+47"
          className={styles.prefix}
          onChange={(e) => {
            if (e.target.value.length == 1) {
              if (isNaN(Number(e.target.value))) {
                setCountryCide(0);
                return;
              }
              setCountryCide(Number(e.target.value));
              return;
            }
            const plussRemoved = e.target.value.substring(1);
            setCountryCide(Number(plussRemoved));
          }}
          type="text"
          id="start"
          name="trip-start"
          value={cuntryCode == 0 ? "" : `+${cuntryCode}`}
        />
        <input
          placeholder="90732947"
          className={styles.input}
          onChange={(e) => {
            setTlf(Number(e.target.value));
          }}
          type="text"
          id="start"
          name="trip-start"
          value={tlf == 0 ? "" : tlf}
        />
      </div>
    </div>
  );
};

export default TlfInput;

/*
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

*/
