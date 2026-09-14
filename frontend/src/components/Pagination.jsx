function Pagination({ page, onPrevious, onNext }) {
  return (
    <div>
        <button onClick={onPrevious} disabled={page === 1}>
        Previous
        </button>  
        <span> Page {page} </span>
        <button onClick={onNext}>Next</button>
    </div>
  )
}

export default Pagination