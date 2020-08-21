import axios from "axios";

export default axios.create({
  baseURL: `http://10.26.1.141:5000/api/`,
});
