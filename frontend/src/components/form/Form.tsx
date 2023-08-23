import { useEffect, useState } from "react";
import styles from "./Form.module.css";
import AgeInput from "../ageInput/AgeInput";
import DateInput from "../dateInput/DateInput";
import SingleStringInput from "../singleStringInput/SingleStringInput";
import TlfInput from "../tlfInput/TlfInput";
import Hidden from "../hidden/Hidden";

const Form = () => {
  const [firstName, setFirstName] = useState("");
  const [middleName, setMiddleName] = useState("");
  const [lastName, setLastName] = useState("");
  const [age, setAge] = useState(0);
  const [birthdate, setBirthdate] = useState(new Date());
  const [email, setEmail] = useState("");
  const [tlf, setTlf] = useState(0);
  const [cuntryCode, setCuntryCode] = useState(0);

  const subnit = () => {
    if (!validateAllData()) return;
    const message = {
      applicant: "Johnny Bakaas",
      email: email,
      name: fullNameConstructor(),
      phone: "+4790761610",
    };
    console.log(message);
  };

  const fullNameConstructor = () => {
    let outP = firstName;
    if (middleName != "") outP += " " + middleName;
    outP += " " + lastName;
    return outP;
  };

  const validateAllData = () => {
    return false;
    if (validateName(firstName)) return false;
    if (validateName(lastName)) return false;
  };

  const validateName = (name: string) => {
    const norgesLengsteNavn = 69;
    if (name.length >= norgesLengsteNavn) return false;
    const regex = /^[a-zA-ZæøåÆØÅ]+(\-|\s?[a-zA-ZæøåÆØÅ]+)+?$/;
    return name.match(regex);
  };

  const validateAge = (numb: number) => {
    const oldestPersoneAge = 116;
    // 10 år burde være en god buffer
    if (numb < oldestPersoneAge + 10) return true;
    return false;
  };

  const validateEmail = (email: string) => {
    const regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return email.match(regex);
  };

  const validatePhoneNr = (pre: number, num: number) => {
    // Only validates 8 digets number seeing how the component is only designed for that
    if (pre <= 0 || pre >= 600) return false;
    if (String(num).length == 8) return true;
    return false;
  };

  return (
    <form className={styles.form} onSubmit={(e) => e.preventDefault()}>
      <Hidden />
      <h1>Min kjempe kule form</h1>
      <h3>
        Dette er en kjempe kul form som virkelig kunne trengt en UX designer,
        men jeg har dark mode, så det er ganske kult
      </h3>
      <SingleStringInput
        description="Forrnavn"
        placeHolder="Ola"
        name={firstName}
        setName={setFirstName}
      />
      {validateName(firstName) ? null : (
        <p>Navn kan ikke inneholde tall og spesialkarakterer</p>
      )}

      <SingleStringInput
        optional
        description="Mellomnavn"
        placeHolder="Kåre"
        name={middleName}
        setName={setMiddleName}
      />
      {middleName == "" ? null : validateName(middleName) ? null : (
        <p>Navn kan ikke inneholde tall og spesialkarakterer</p>
      )}

      <SingleStringInput
        description="Etternavn"
        placeHolder="Nordmann"
        name={lastName}
        setName={setLastName}
      />
      {validateName(lastName) ? null : (
        <p>Navn kan ikke inneholde tall og spesialkarakterer</p>
      )}

      <AgeInput val={age} setVal={setAge} optional />
      {validateAge(age) ? null : <p>Du er ALT for gammel</p>}

      <DateInput
        optional
        description="Når ble du født? "
        date={birthdate}
        setDate={setBirthdate}
      />

      <SingleStringInput
        description="Epost"
        placeHolder="Ola@Nordmann.gov"
        name={email}
        setName={setEmail}
      />
      {validateEmail(email) ? null : (
        <p>Navn kan ikke inneholde tall og spesialkarakterer</p>
      )}

      <TlfInput
        tlf={tlf}
        setTlf={setTlf}
        cuntryCode={cuntryCode}
        setCountryCide={setCuntryCode}
      />
      {validatePhoneNr(cuntryCode, tlf) ? null : <p>Ugyldig tlf</p>}

      <button
        className={styles.button}
        onClick={() => {
          subnit();
        }}
      >
        SEND
      </button>
    </form>
  );
};

export default Form;
