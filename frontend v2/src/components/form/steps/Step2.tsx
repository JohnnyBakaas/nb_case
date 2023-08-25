import { useEffect } from "react";
import styles from "./Step2.module.css";

type Props = {
  aboutTheJob: string;
  setAboutTheJob: (e: string) => void;

  setNextAvailable: (pre: number) => void;
};

const Step2 = ({
  aboutTheJob,
  setAboutTheJob,
  setNextAvailable: setNextAwaleble,
}: Props) => {
  useEffect(() => {
    setNextAwaleble(2);
  }, []);
  return (
    <>
      <h3>Fortell oss om jobben</h3>
      <textarea
        className={styles.textarea}
        value={aboutTheJob}
        onChange={(e) => {
          setAboutTheJob(e.target.value);
        }}
      ></textarea>
    </>
  );
};

export default Step2;
