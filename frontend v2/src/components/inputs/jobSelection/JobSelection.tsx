import styles from "./JobSelection.module.css";

type JobSelectionProps = {
  jobs: string[];
  job: string;
  setJob: (e: string) => void;
};

const JobSelection = ({ jobs, job, setJob }: JobSelectionProps) => {
  console.log(job);
  return (
    <table className={styles.table}>
      <tbody>
        {jobs.map((jobName, i) => {
          return (
            <tr key={i}>
              <td>
                <input
                  type="radio"
                  id={jobName}
                  name="jobSelection"
                  onChange={() =>
                    setJob(
                      jobName.charAt(0).toUpperCase() +
                        jobName.slice(1).toLowerCase()
                    )
                  }
                />
                <label htmlFor={jobName}>
                  {" "}
                  {jobName.charAt(0).toUpperCase() +
                    jobName.slice(1).toLowerCase()}
                </label>
              </td>
            </tr>
          );
        })}
        <tr>
          <td>
            <input
              style={job === "annet" ? {} : { display: "none" }}
              type="text"
            />

            <>
              <input
                type="radio"
                id="Annet"
                name="jobSelection"
                style={job === "annet" ? { display: "none" } : {}}
                onChange={() => setJob("annet")}
              />
              <label
                htmlFor="Annet"
                style={job === "annet" ? { display: "none" } : {}}
              >
                {" "}
                Annet?
              </label>
            </>
          </td>
        </tr>
      </tbody>
    </table>
  );
};

export default JobSelection;
