import './RequestFilters.css';

function RequestFilters({
  requestNumber,
  onRequestNumberChange,
  onSearch,
  statuses,
  onStatusChange,
  requestType,
  onRequestTypeChange,
  fromDate,
  onFromDateChange,
  toDate,
  onToDateChange,
  sortBy,
  onSortByChange,
  sortDescending,
onSortDescendingChange,
}) {
     return (
    <div className="filters">
        <h2>Filters</h2>
        <div className="filter-row">
            <label>
                Request Number:
                <input
                    type="text"
                    value={requestNumber}
                    onChange={(event) => onRequestNumberChange(event.target.value)}
                    placeholder="Search by request number"
                    />

                <button type="button" onClick={onSearch}>
                    Search
                </button>
            </label>
            <div>
                <span>Status:</span>
                <label>
                    <input
                    type="checkbox"
                    checked={statuses.includes(1)}
                    onChange={() => onStatusChange(1)}
                    />
                    New 
                </label>

                <label>
                    <input
                        type="checkbox"
                        checked={statuses.includes(2)}
                        onChange={() => onStatusChange(2)}
                    />
                    In Progress
                </label>

                <label>
                    <input
                        type="checkbox"
                        checked={statuses.includes(3)}
                        onChange={() => onStatusChange(3)}
                    />
                    Completed
                </label>

                <label>
                    <input
                        type="checkbox"
                        checked={statuses.includes(4)}
                        onChange={() => onStatusChange(4)}
                    />
                    Cancelled
                </label>
                </div>
                <div>
                    <label>
                        Request Type:
                        <select
                        value={requestType}
                        onChange={(event) => onRequestTypeChange(event.target.value)}
                        >   
                            <option value="">All</option>
                            <option value="1">General</option>
                            <option value="2">Legal</option>
                            <option value="3">Payment</option>
                            <option value="4">Appeal</option>
                        </select>
                    </label>
            </div>
        </div>

            <div className="filter-row">
                <label>
                    From Date:
                    <input
                    type="date"
                    value={fromDate}
                    onChange={(event) => onFromDateChange(event.target.value)}
                    />
                </label>

                <label>
                    To Date:
                    <input
                    type="date"
                    value={toDate}
                    onChange={(event) => onToDateChange(event.target.value)}
                    />
                </label>
        </div>

        <div className="filter-row">
            <label>
                Sort Direction:
                <select
                    value={sortDescending}
                    onChange={(event) =>
                        onSortDescendingChange(event.target.value === 'true')
                    }
                    >
                <option value="false">Ascending</option>
                <option value="true">Descending</option>
                </select>
            </label>
        </div>

        <div>
            <label>
                Sort By:
                <select
                    value={sortBy}
                    onChange={(event) => onSortByChange(event.target.value)}
                    >
                <option value="">Default</option>
                <option value="requestnumber">Request Number</option>
                <option value="createdat">Created At</option>
                <option value="status">Status</option>
                <option value="requesttype">Request Type</option>
                </select>
            </label>
        </div>
    </div>
  )
}

export default RequestFilters
