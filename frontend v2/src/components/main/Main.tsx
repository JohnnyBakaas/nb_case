import styles from "./Main.module.css";
import backgroundVideo from "../../assets/y2mate.is - diy_fails___work___home_improvement_fails_compilation-Ojxe9dIioUg-1080pp-1692877637.mp4";
import Form from "../form/Form";

const Main = () => {
  return (
    <main className={styles.main}>
      <video autoPlay muted loop style={{ backgroundColor: "purple" }}>
        {<source src={backgroundVideo} type="video/mp4" />}
      </video>
      <div className={styles.content}>
        <div>
          <h2>Noen ting er det best og etterlate til proffene</h2>

          <p>
            Trenger du hjelp til oppgaver som krever ekspertise og
            profesjonalitet? Velkommen til ProffSentralen, hvor dedikerte
            fagfolk fra ulike bransjer står klare til å assistere deg.
          </p>
          <p>
            Vi kobler deg med erfarne eksperter som virkelig behersker sine
            fagfelt, enten det er medisinsk rådgivning, juridisk veiledning,
            tekniske reparasjoner eller kreative tjenester. Våre nøye utvalgte
            proffer har den rette kompetansen og erfaringen for oppgaven.
          </p>
        </div>
        <Form />
      </div>
    </main>
  );
};

export default Main;
