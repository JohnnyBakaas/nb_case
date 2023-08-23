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
