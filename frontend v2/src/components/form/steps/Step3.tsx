import { useEffect } from "react";
import styles from "./Step3.module.css";

type Props = {
  firstName: string;
  setFirstName: (val: string) => void;
  midleName: string;
  setMidleName: (val: string) => void;
  lastName: string;
  setLastName: (val: string) => void;
  email: string;
  setEmail: (email: string) => void;
  phoneNumber: number[];
  setPhoneNumber: React.Dispatch<React.SetStateAction<number[]>>;

  setNextAvailable: (pre: number) => void;
};

const Step3 = ({
  firstName,
  setFirstName,
  midleName,
  setMidleName,
  lastName,
  setLastName,
  email,
  setEmail,
  phoneNumber,
  setPhoneNumber,

  setNextAvailable,
}: Props) => {
  const validateFirstName = () => {
    const nameRegex = /^[a-zA-Z ]+$/;
    if (firstName.length <= 1 || !nameRegex.test(firstName)) return false;
    return true;
  };

  const validateMiddleName = () => {
    const nameRegex = /^[a-zA-Z ]+$/;
    if (midleName && (midleName.length <= 1 || !nameRegex.test(midleName)))
      return false;
    return true;
  };

  const validateLastName = () => {
    const nameRegex = /^[a-zA-Z ]+$/;
    if (lastName.length <= 1 || !nameRegex.test(lastName)) return false;
    return true;
  };

  const validateEmail = () => {
    const emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    return emailRegex.test(email);
  };

  const validatePhoneNumber = () => {
    return (
      String(phoneNumber[0]).length <= 3 && String(phoneNumber[1]).length === 8
    );
  };

  const validateAllData = () => {
    if (
      !validateFirstName() ||
      !validateMiddleName() ||
      !validateLastName() ||
      !validateEmail() ||
      !validatePhoneNumber()
    )
      return;

    console.log("Det fungerer");
    setNextAvailable(3);
  };

  useEffect(validateAllData, [
    firstName,
    midleName,
    lastName,
    email,
    phoneNumber,
  ]);

  return (
    <>
      <h3>Hvem er du?</h3>
      <section className={styles.section}>
        <label htmlFor="firstName">Fornavn</label>
        <input
          type="text"
          id="firstName"
          value={firstName}
          onChange={(e) => {
            setFirstName(e.target.value);
          }}
        />
        {validateFirstName() ? null : (
          <p className="missing-information">
            Navn kan bare inneholde bokstaver og minimum lengde på 2 bokstaver
          </p>
        )}
      </section>

      <section className={styles.section}>
        <label htmlFor="secontName">Mellomnavn</label>
        <input
          type="text"
          id="secontName"
          value={midleName}
          onChange={(e) => {
            setMidleName(e.target.value);
          }}
        />
        {validateMiddleName() ? null : (
          <p className="missing-information">
            Navn kan bare inneholde bokstaver og minimum lengde på 2 bokstaver
          </p>
        )}
      </section>

      <section className={styles.section}>
        <label htmlFor="lastName">Etternavn</label>
        <input
          type="text"
          id="lastName"
          value={lastName}
          onChange={(e) => {
            setLastName(e.target.value);
          }}
        />
        {validateLastName() ? null : (
          <p className="missing-information">
            Navn kan bare inneholde bokstaver og minimum lengde på 2 bokstaver
          </p>
        )}
      </section>

      <h3>Kontakt informasjon</h3>

      <section className={styles.section}>
        <label htmlFor="email">E-post</label>
        <input
          type="email"
          id="email"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />
        {validateEmail() ? null : (
          <p className="missing-information">Må være en gyldig email </p>
        )}
      </section>

      <section className={styles.section}>
        <label htmlFor="phoneNumber">Telefon</label>
        <span className={styles.tlf}>
          <input
            placeholder="+47"
            type="tel"
            id="phonePrefix"
            value={phoneNumber[0] == 0 ? "" : "+" + phoneNumber[0].toString()}
            onChange={(e) => {
              var newNumber;
              if (e.target.value.includes("+")) {
                newNumber = Number(e.target.value.substring(1));
              } else {
                newNumber = Number(e.target.value);
              }
              setPhoneNumber((pre) => [
                Number.isNaN(newNumber) ? pre[0] : newNumber,
                pre[1],
              ]);
            }}
          />
          <input
            placeholder="90732947"
            type="tel"
            id="phoneNumber"
            value={phoneNumber[1] == 0 ? "" : phoneNumber[1].toString()}
            onChange={(e) => {
              const newNumber = Number(e.target.value);
              setPhoneNumber((pre) => [
                pre[0],
                Number.isNaN(newNumber) ? pre[1] : newNumber,
              ]);
            }}
          />
        </span>
        {validatePhoneNumber() ? null : (
          <p className="missing-information">
            Må være ett gyldig nordisk nummer
          </p>
        )}
      </section>
    </>
  );
};

export default Step3;
