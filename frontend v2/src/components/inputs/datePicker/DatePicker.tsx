import styles from "./DatePicker.module.css";

type DatePickerProps = {
  date: Date;
  setDate: (date: Date) => void;
};

const DatePicker = ({ date, setDate }: DatePickerProps) => {
  return (
    <>
      <h3>Når</h3>
      <input
        type="date"
        name="test"
        id="k"
        className={styles.fix}
        onChange={(e) => setDate(new Date(Date.parse(e.target.value)))}
        min={new Date().toISOString().split("T")[0]}
        value={date.toISOString().split("T")[0]}
      />
    </>
  );
};
export default DatePicker;

/*
import styles from "./DateInput.module.css";

interface DateInputProps {
  description: string;
  optional?: boolean;
  date: Date;
  setDate: (date: Date) => void;
}

const DateInput = ({
  optional,
  description,
  date,
  setDate,
}: DateInputProps) => {
  return (
    <div>
      <label htmlFor="ageInput" className={styles.label}>
        {optional === undefined ? "*" : null} {description}
      </label>
      <input
        className={styles.date}
        onChange={(e) => setDate(new Date(Date.parse(e.target.value)))}
        type="date"
        id="start"
        name="trip-start"
        value={date.toISOString().split("T")[0]}
      />
    </div>
  );
};

export default DateInput;

*/
