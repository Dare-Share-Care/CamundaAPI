# Customer Complaints Handling Workflow

## Authors

- Frederik Andersen
- Janus Rasmussen
- Julius Krüger

## Business Scenario

This workflow handles customer complaints from the point they're received till their resolution. The process involves reviewing, investigating, and resolving the complaint while potentially needing to contact the customer for additional information.

## BPMN Design

The BPMN diagram represents the entire complaints handling process with decision nodes determining if further information is needed from the customer.

## Prerequisites

- Java 8 or above.
- Camunda Platform Runtime.
- External services API credentials.

## Setup and Running

1. Deploy the BPMN process to Camunda.
2. Configure the necessary delegates.
3. Start the Camunda platform and initiate the process through the Camunda Tasklist.
