import React from "react";
import AdressPicker from "./AdressPicker";
import styles from "./AdressPicker.module.css";

type AdressPickerWrapperProps = {};

const AdressPickerWrapper = () => {
  return (
    <>
      <h3>Hvor</h3>
      <section className={styles.power}>
        <div className={styles.wrapper}>
          <AdressPicker />
        </div>
      </section>
    </>
  );
};

export default AdressPickerWrapper;
