// usePostRequest.ts
import { useState } from 'react';
import axios from 'axios';

type PostResponse = {
  endpoint1Data: any;
};

const usePostRequest = () => {
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<Error | null>(null);

  const sendPostRequest = async (endpoint1: string, data: any): Promise<PostResponse | null> => {
    setLoading(true);
    setError(null);

    try {
      const response1 = await axios.post(endpoint1, data);

      setLoading(false);
      return {
        endpoint1Data: response1.data,
      };
    } catch (err: any) {
      setLoading(false);
      setError(err);
      return null;
    }
  };

  return { sendPostRequest, loading, error };
};

export default usePostRequest;
