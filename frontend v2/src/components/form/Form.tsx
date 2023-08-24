import { useState } from "react";
import styles from "./Form.module.css";
import Page1 from "./pages/Page1";

const jobSelectionOptions = [
  "Snekker",
  "Rørlegger",
  "elekTrikker",
  "bil makaniker",
];

const Form = () => {
  const [job, setJob] = useState("");
  const handleSubmit = () => {};
  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <Page1
        job={job}
        setJob={setJob}
        jobSelectionOptions={jobSelectionOptions}
      />
    </form>
  );
};

export default Form;

/* Hva skal inn

Dropdown med forkjellige typer jobber (kan velge selv)

beskrivelse av problemet text

Dato picker for når man vil ha besøk

*/
