import { FormEvent, useState } from "react";
import styles from "./Form.module.css";
import Step1 from "./steps/Step1";
import Step2 from "./steps/Step2";
import Step3 from "./steps/Step3";
import usePostRequest from "../../hooks/usePostRequest";

const jobSelectionOptions = [
  "Snekker",
  "Rørlegger",
  "elekTrikker",
  "bilmekaniker",
];

const Form = () => {
  const [step, setStep] = useState(0);
  const [nextAvailable, setNextAvailable] = useState(0);
  const [job, setJob] = useState("");
  const [date, setDate] = useState(new Date());

  const [aboutTheJob, setAboutTheJob] = useState("");

  const [firstName, setFirstName] = useState("");
  const [midleName, setMidleName] = useState("");
  const [lastName, setLastName] = useState("");

  const [email, setEmail] = useState("");
  const [phoneNumber, setPhoneNumber] = useState<number[]>([47, 0]);

  const [formStatus, setFormStatus] = useState("");

  const { sendPostRequest, loading, error } = usePostRequest();

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const formData = {
      applicant: "Johnny Bakaas",
      job,
      date,
      aboutTheJob,
      firstName,
      midleName,
      lastName,
      email,
      phoneNumberPrefix: phoneNumber[0],
      phoneNumber: phoneNumber[1],
    };

    const result = await sendPostRequest(
      "https://localhost:7223/api/v2/ContactForm/Submit",
      formData
    );
    if (result) {
      console.log("Data sent successfully!");
      setFormStatus("sucsess");
    }
  };

  const steps = [
    <Step1
      job={job}
      setJob={setJob}
      jobSelectionOptions={jobSelectionOptions}
      date={date}
      setDate={setDate}
      setNextAvailable={setNextAvailable}
    />,
    <Step2
      aboutTheJob={aboutTheJob}
      setAboutTheJob={setAboutTheJob}
      setNextAvailable={setNextAvailable}
    />,
    <Step3
      firstName={firstName}
      setFirstName={setFirstName}
      midleName={midleName}
      setMidleName={setMidleName}
      lastName={lastName}
      setLastName={setLastName}
      email={email}
      setEmail={setEmail}
      phoneNumber={phoneNumber}
      setPhoneNumber={setPhoneNumber}
      setNextAvailable={setNextAvailable}
    />,
    ,
  ];

  if (error) {
    return (
      <div>
        <h1>Noe har gått galt!</h1>
        {error.message}
      </div>
    );
  }

  if (loading) {
    return (
      <div>
        <h1>fancy loading greie</h1>
        <p>Klokka er 21:26</p>
        <p>Jeg har ikke tid til å gjøre noe fancy greier</p>
      </div>
    );
  }

  if (formStatus === "sucsess")
    return (
      <div>
        <h1>Takk!</h1>
        <p>Du kan forvente svar innen 14-32 arbeidsdager</p>
      </div>
    );

  return (
    <form className={styles.form} onSubmit={handleSubmit}>
      <section>{steps[step]}</section>

      <section className={styles.buttonWrapper}>
        {step !== 0 && (
          <button
            onClick={() => {
              setStep((prev) => prev - 1);
            }}
          >
            Forige
          </button>
        )}
        {step === steps.length - 2 ? (
          <button disabled={!(nextAvailable === step + 1)}>Send</button>
        ) : (
          <button
            onClick={() => {
              setStep((prev) => prev + 1);
            }}
            disabled={!(nextAvailable === step + 1)}
          >
            Neste
          </button>
        )}
      </section>
    </form>
  );
};

export default Form;

/* Hva skal inn

Dropdown med forkjellige typer jobber (kan velge selv)

beskrivelse av problemet text

Dato picker for når man vil ha besøk

*/
