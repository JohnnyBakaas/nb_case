import JobSelection from "../../inputs/jobSelection/JobSelection";

type Page1Props = {
  jobSelectionOptions: string[];
  job: string;
  setJob: (e: string) => void;
};

const Page1 = ({ jobSelectionOptions, job, setJob }: Page1Props) => {
  return (
    <>
      <JobSelection jobs={jobSelectionOptions} job={job} setJob={setJob} />
    </>
  );
};

export default Page1;

/* Hva skal inn

Dropdown med forkjellige typer jobber (kan velge selv)

beskrivelse av problemet text

Dato picker for når man vil ha besøk

*/
