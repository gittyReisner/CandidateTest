function RequestsTable({ requests }) {
  return (
    <table>
      <thead>
        <tr>
          <th>Request Number</th>
          <th>Status</th>
          <th>Request Type</th>
          <th>Created At</th>
        </tr>
      </thead>

      <tbody>
        {requests.map((request) => (
          <tr key={request.id}>
            <td>{request.requestNumber}</td>
            <td>{request.status}</td>
            <td>{request.requestType}</td>
            <td>{request.createdAt}</td>
          </tr>
        ))}
      </tbody>
    </table>
  )
}

export default RequestsTable
