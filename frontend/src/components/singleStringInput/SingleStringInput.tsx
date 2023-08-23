import styles from "./SingleStringInput.module.css";

interface NameInputProps {
  optional?: boolean;
  description: string;
  placeHolder: string;
  name: string;
  setName: (name: string) => void;
}

const SingleStringInput = ({
  optional,
  description,
  name,
  placeHolder,
  setName,
}: NameInputProps) => {
  return (
    <div className={styles.wrapper}>
      <label htmlFor="nameInput" className={styles.lable}>
        {optional === undefined ? "*" : null} {description}
      </label>
      <input
        className={styles.input}
        id="nameInput"
        type="text"
        value={name}
        placeholder={placeHolder}
        onChange={(e) => setName(e.target.value)}
      />
    </div>
  );
};

export default SingleStringInput;
