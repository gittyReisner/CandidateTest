import { useEffect, useState } from 'react';
import RequestsTable from './components/RequestsTable';
import RequestFilters from './components/RequestFilters';
import Pagination from './components/Pagination';
import './App.css'

function App() {
  const [requests, setRequests] = useState([]);
  const [requestNumber, setRequestNumber] = useState('');
  const [searchRequestNumber, setSearchRequestNumber] = useState('');
  const [statuses, setStatuses] = useState([]);
  const [requestType, setRequestType] = useState('');
  const [fromDate, setFromDate] = useState('');
  const [toDate, setToDate] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [sortBy, setSortBy] = useState('');
  const [sortDescending, setSortDescending] = useState(false);
  const [page, setPage] = useState(1);

  const handlePreviousPage = () => {
    setPage((currentPage) => Math.max(currentPage - 1, 1))
  }

  const handleNextPage = () => {
    setPage((currentPage) => currentPage + 1)
  }

  const handleStatusChange = (status) => {
    setStatuses((currentStatuses) => {
      if (currentStatuses.includes(status)) {
        return currentStatuses.filter((item) => item !== status)
      }

      return [...currentStatuses, status]
    })
  }

  useEffect(() => {
    setLoading(true);
    setError('');
    
    const url = new URL('https://localhost:44305/api/Requests')

    if (searchRequestNumber) {
      url.searchParams.append('RequestNumber', searchRequestNumber)
    }

    statuses.forEach((status) => {
      url.searchParams.append('Statuses', status)
    })

    if (requestType) {
      url.searchParams.append('RequestType', requestType)
    }

    if (sortBy) {
      url.searchParams.append('SortBy', sortBy)
    }
    url.searchParams.append('SortDescending', sortDescending)

    if (fromDate) {
      url.searchParams.append('FromDate', fromDate)
    }

    if (toDate) {
      url.searchParams.append('ToDate', toDate)
    }

    url.searchParams.append('Page', page);

  fetch(url, {
    headers: {
      'X-User-Id': '1',
      'X-Is-Admin': 'true',
    },
  })
    .then(async (response) => {
      if (!response.ok) {
        const message = await response.text()
        throw new Error(message)
      }

      return response.json()
    })
  .then((data) => {
    setRequests(data)
    setLoading(false)
  })
  .catch((error) => {
    console.error(error)
    setRequests([])
    setError(error.message)
    setLoading(false)
  })
  }, [searchRequestNumber, statuses, requestType, fromDate, toDate, sortBy, sortDescending, page])

  return (
    <div className="app">
      <h1>Requests</h1>

      <RequestFilters
        requestNumber={requestNumber}
        onRequestNumberChange={setRequestNumber}
        onSearch={() => {
          setPage(1)
          setSearchRequestNumber(requestNumber)
        }}
        statuses={statuses}
        onStatusChange={handleStatusChange}
        requestType={requestType}
        onRequestTypeChange={setRequestType}
        fromDate={fromDate}
        onFromDateChange={setFromDate}
        toDate={toDate}
        onToDateChange={setToDate}
        sortBy={sortBy}
        onSortByChange={setSortBy}
        sortDescending={sortDescending}
        onSortDescendingChange={setSortDescending}
      />

      {loading && <p>Loading...</p>}

      {error && <p>Error: {error}</p>}

      {!loading && !error && requests.length === 0 && (
        <p>No results found.</p>
      )}

      {!loading && !error && requests.length > 0 && (
        <RequestsTable requests={requests} />
      )}

      <Pagination
        page={page}
        onPrevious={handlePreviousPage}
        onNext={handleNextPage}
      />
    </div>
  )
}

export default App
