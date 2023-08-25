import React, { useEffect } from "react";
import AdressPicker from "../../inputs/adressPicker/AdressPickerWrapper";
import DatePicker from "../../inputs/datePicker/DatePicker";
import JobSelection from "../../inputs/jobSelection/JobSelection";

type Props = {
  jobSelectionOptions: string[];
  job: string;
  setJob: (e: string) => void;

  date: Date;
  setDate: (date: Date) => void;

  setNextAvailable: (pre: number) => void;
};

const Step1 = ({
  jobSelectionOptions,
  job,
  setJob,
  date,
  setDate,
  setNextAvailable: setNextAwaleble,
}: Props) => {
  useEffect(() => {
    if (job === "" || job === "annet") {
      setNextAwaleble(0);
    } else setNextAwaleble(1);
  }, [job, setNextAwaleble]);

  return (
    <>
      <JobSelection jobs={jobSelectionOptions} job={job} setJob={setJob} />
      <DatePicker date={date} setDate={setDate} />
      <AdressPicker />
    </>
  );
};

export default Step1;
