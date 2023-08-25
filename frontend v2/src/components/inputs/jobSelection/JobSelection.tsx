type Props = {
  jobs: string[];
  job: string;
  setJob: (e: string) => void;
};

const JobSelection = ({ jobs, job, setJob }: Props) => {
  console.log(job);
  return (
    <>
      <h3>Hvem trenger du?</h3>
      <table>
        <tbody>
          {jobs.map((jobName, i) => {
            return (
              <tr key={i}>
                <td>
                  <input
                    type="radio"
                    id={jobName}
                    name="jobSelection"
                    onChange={() => setJob(jobName)}
                    checked={job === jobName}
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
                style={
                  job === ""
                    ? { display: "none" }
                    : job == "annet"
                    ? {}
                    : jobs.includes(job)
                    ? { display: "none" }
                    : {}
                }
                placeholder="Kake baker"
                type="text"
                value={job == "annet" ? "" : jobs.includes(job) ? "" : job}
                onChange={(e) => {
                  setJob(e.target.value);
                }}
              />

              <>
                <input
                  type="radio"
                  id="Annet"
                  name="jobSelection"
                  style={
                    job === ""
                      ? {}
                      : !jobs.includes(job)
                      ? { display: "none" }
                      : {}
                  }
                  onChange={() => setJob("annet")}
                  checked={
                    job === "" ? false : jobs.includes(job) ? false : true
                  }
                />
                <label
                  htmlFor="Annet"
                  style={
                    job === ""
                      ? {}
                      : !jobs.includes(job)
                      ? { display: "none" }
                      : {}
                  }
                >
                  {" "}
                  Annet?
                </label>
              </>
            </td>
          </tr>
        </tbody>
      </table>
      {job === "" || job === "annet" ? (
        <p className="missing-information">Du må svare</p>
      ) : null}
    </>
  );
};

export default JobSelection;
